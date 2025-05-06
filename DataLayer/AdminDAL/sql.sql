USE TOURZY
GO


CREATE PROCEDURE sp_DangNhap
	@TenDangNhap NVARCHAR(50),
	@MatKhau NVARCHAR(50)
AS
BEGIN
	SELECT *
	FROM TAIKHOAN
	WHERE TenDangNhap = @TenDangNhap
	  AND MatKhau = @MatKhau
	  AND IsDeleted = 0
END
GO

CREATE PROCEDURE sp_KiemTraTaiKhoan
	@TenDangNhap NVARCHAR(50)
AS
BEGIN
	SELECT COUNT(*)
	FROM TAIKHOAN
	WHERE TenDangNhap = @TenDangNhap
END
GO

CREATE PROCEDURE sp_DangKy
	@TenDangNhap NVARCHAR(50),
	@MatKhau NVARCHAR(50),
	@VaiTro NVARCHAR(50),
	@IsDeleted BIT
AS
BEGIN
	INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, VaiTro, IsDeleted)
	VALUES (@TenDangNhap, @MatKhau, @VaiTro, @IsDeleted)
END
GO

CREATE PROCEDURE sp_UpdateOTP
    @TenDangNhap NVARCHAR(50),
    @OTP NVARCHAR(10),
    @Expiry DATETIME
AS
BEGIN
    UPDATE TaiKhoan
    SET ResetOTP = @OTP,
        OTPExpiry = @Expiry
    WHERE TenDangNhap = @TenDangNhap
END
GO

CREATE PROCEDURE sp_KiemTra
    @TenDangNhap NVARCHAR(50),
    @Email NVARCHAR(100)
AS
BEGIN
    SELECT COUNT(*)
    FROM TaiKhoan tk
    JOIN ThongTinCaNhan ttcn ON tk.ID = ttcn.MaTaiKhoan
    WHERE tk.TenDangNhap = @TenDangNhap AND ttcn.Email = @Email
END
GO

CREATE PROCEDURE sp_XacNhanOTP
    @TenDangNhap NVARCHAR(50)
AS
BEGIN
    SELECT ResetOTP, OTPExpiry
    FROM TaiKhoan
    WHERE TenDangNhap = @TenDangNhap
END
GO

CREATE PROCEDURE sp_LayMatKhau
    @TenDangNhap NVARCHAR(50)
AS
BEGIN
    SELECT MatKhau
    FROM TaiKhoan
    WHERE TenDangNhap = @TenDangNhap
END
GO

CREATE PROCEDURE sp_GetAllTours
AS
BEGIN
    SELECT *
    FROM ChuyenDi
END
GO

CREATE PROCEDURE sp_GetNameTour
    @MaChuyenDi NVARCHAR(50)
AS
BEGIN
    SELECT MaChuyenDi,
           TenChuyenDi,
           HinhThuc,
           HanhTrinh,
           SoNgayDi,
           Gia,
           SoLuong,
           ChiTiet,
           MoTa
    FROM ChuyenDi
    WHERE MaChuyenDi = @MaChuyenDi
END
GO

CREATE PROCEDURE sp_AddTour
    @MaChuyenDi NVARCHAR(10),
    @TenChuyenDi NVARCHAR(100),
    @HinhThuc NVARCHAR(50),
    @HanhTrinh NVARCHAR(255),
    @SoNgayDi INT,
    @Gia INT,
    @SoLuong INT,
    @ChiTiet NVARCHAR(MAX),
    @MoTa NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO ChuyenDi(MaChuyenDi, TenChuyenDi, HinhThuc, HanhTrinh, SoNgayDi, Gia, SoLuong, ChiTiet, MoTa)
    VALUES (@MaChuyenDi, @TenChuyenDi, @HinhThuc, @HanhTrinh, @SoNgayDi, @Gia, @SoLuong, @ChiTiet, @MoTa)
END
GO

CREATE PROCEDURE sp_UpdateTour
    @MaChuyenDi NVARCHAR(10),
    @TenChuyenDi NVARCHAR(100),
    @HinhThuc NVARCHAR(50),
    @HanhTrinh NVARCHAR(255),
    @SoNgayDi INT,
    @Gia INT,
    @SoLuong INT,
    @ChiTiet NVARCHAR(MAX),
    @MoTa NVARCHAR(MAX)
AS
BEGIN
    UPDATE ChuyenDi
    SET TenChuyenDi = @TenChuyenDi,
        HinhThuc = @HinhThuc,
        HanhTrinh = @HanhTrinh,
        SoNgayDi = @SoNgayDi,
        Gia = @Gia,
        SoLuong = @SoLuong,
        ChiTiet = @ChiTiet,
        MoTa = @MoTa
    WHERE MaChuyenDi = @MaChuyenDi
END
GO

CREATE PROCEDURE sp_DeleteTour
    @MaChuyenDi NVARCHAR(50)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM DanhSachDangKy WHERE MaChuyenDi = @MaChuyenDi;

        DELETE FROM LichTrinh WHERE MaChuyenDi = @MaChuyenDi;

        DELETE FROM DanhSachDuKhach WHERE MaChuyenDi = @MaChuyenDi;

        DELETE FROM DanhGia WHERE MaChuyenDi = @MaChuyenDi;

        DELETE FROM ChuyenDi WHERE MaChuyenDi = @MaChuyenDi;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Error: ' + ERROR_MESSAGE();
    END CATCH;
END
GO

CREATE PROCEDURE sp_GetAccounts
AS
BEGIN
    SELECT ID, TenDangNhap, MatKhau, VaiTro, IsDeleted
    FROM TaiKhoan
    WHERE IsDeleted = 0
END
GO

CREATE PROCEDURE sp_GetInfoByAccountID
    @MaTaiKhoan INT
AS
BEGIN
    SELECT MaTaiKhoan, Ten, SDT, DiaChi, Email FROM ThongTinCaNhan
    WHERE MaTaiKhoan = @MaTaiKhoan
END
GO

CREATE PROCEDURE sp_GetToursByAccount
    @MaTaiKhoan INT
AS
BEGIN
    SELECT MaTaiKhoan, MaChuyenDi, NgayBatDau, SoLuong, TrangThai
    FROM DanhSachDangKy
    WHERE MaTaiKhoan = @MaTaiKhoan
END
GO

CREATE PROCEDURE sp_DeleteAccount
    @MaTaiKhoan INT
AS
BEGIN
    DELETE FROM TaiKhoan WHERE ID = @MaTaiKhoan;
END
GO


CREATE PROCEDURE sp_GetReviews
AS
BEGIN
    SELECT 
        dg.MaChuyenDi,
        cd.TenChuyenDi,
        dg.MaTaiKhoan,
        ttcn.Ten AS TenNguoiDanhGia,
        dg.Sao,
        dg.BinhLuan
    FROM DanhGia dg
    JOIN ChuyenDi cd ON dg.MaChuyenDi = cd.MaChuyenDi
    JOIN ThongTinCaNhan ttcn ON dg.MaTaiKhoan = ttcn.MaTaiKhoan
END
GO

CREATE PROCEDURE sp_LoadTours
AS
BEGIN
    SELECT MaChuyenDi
    FROM ChuyenDi
    WHERE SoLuong > 0
END
GO

CREATE PROCEDURE sp_GetReviewsByTour
    @MaChuyenDi NVARCHAR(50)
AS
BEGIN
    SELECT MaChuyenDi,MaTaiKhoan,Sao,BinhLuan
    FROM DanhGia 
    WHERE MaChuyenDi = @MaChuyenDi
END
GO

CREATE PROCEDURE sp_KiemTraTenDangNhapTonTai
    @TenDangNhap NVARCHAR(50)
AS
BEGIN
    SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap
END
GO

CREATE PROCEDURE sp_ThemTaiKhoan
    @TenDangNhap NVARCHAR(50),
    @MatKhau NVARCHAR(100),
    @VaiTro NVARCHAR(20),
    @NewID INT OUTPUT
AS
BEGIN
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro)
    VALUES (@TenDangNhap, @MatKhau, @VaiTro)

    SET @NewID = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE sp_CapNhatThongTinCaNhan
    @MaTaiKhoan INT,
    @Ten NVARCHAR(100),
    @SDT VARCHAR(10),
    @Email NVARCHAR(255),
    @DiaChi NVARCHAR(255)
AS
BEGIN
    UPDATE ThongTinCaNhan
    SET Ten = @Ten,
        SDT = @SDT,
        Email = @Email,
        DiaChi = @DiaChi
    WHERE MaTaiKhoan = @MaTaiKhoan
END
GO

CREATE PROCEDURE sp_ThemThongTinCaNhan
    @MaTaiKhoan INT,
    @Ten NVARCHAR(100),
    @SDT VARCHAR(10),
    @Email NVARCHAR(255),
    @DiaChi NVARCHAR(255)
AS
BEGIN
    INSERT INTO ThongTinCaNhan (MaTaiKhoan, Ten, SDT, Email, DiaChi)
    VALUES (@MaTaiKhoan, @Ten, @SDT, @Email, @DiaChi)
END
GO

CREATE PROCEDURE sp_GetDSDuKhach
AS
BEGIN
    SELECT *
    FROM DanhSachDuKhach
END
GO

CREATE PROCEDURE sp_GetNgayBatDauByTour
    @MaChuyenDi NVARCHAR(50)
AS
BEGIN
    SELECT DISTINCT NgayBatDau
    FROM DanhSachDuKhach
    WHERE MaChuyenDi = @MaChuyenDi
    ORDER BY NgayBatDau
END
GO

CREATE PROCEDURE sp_CountHanhKhach
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATE
AS
BEGIN
    SELECT COUNT(*) AS SoLuong
    FROM DanhSachDuKhach
    WHERE MaChuyenDi = @MaChuyenDi AND CAST(NgayBatDau AS DATE) = @NgayBatDau
END
GO

CREATE PROCEDURE sp_FilterCustomer
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME,
    @CCCD NVARCHAR(20),
    @Ten NVARCHAR(100),
    @Att1 BIT,
    @Att2 BIT,
    @Att3 BIT,
    @Att4 BIT
AS
BEGIN
    SELECT MaChuyenDi, NgayBatDau, CCCD, Ten, SDT
    FROM DanhSachDuKhach
    WHERE (@Att1 = 0 OR MaChuyenDi = @MaChuyenDi)
      AND (@Att2 = 0 OR NgayBatDau = @NgayBatDau)
      AND (@Att3 = 0 OR CCCD = @CCCD)
      AND (@Att4 = 0 OR Ten = @Ten)
END
GO

CREATE PROCEDURE sp_DeleteCustomer
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME,
    @CCCD NVARCHAR(20)
AS
BEGIN
    DELETE FROM DanhSachDuKhach
    WHERE MaChuyenDi = @MaChuyenDi AND NgayBatDau = @NgayBatDau AND CCCD = @CCCD
END
GO

CREATE PROCEDURE AddCustomer
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME,
    @CCCD NVARCHAR(50),
    @Ten NVARCHAR(100),
    @SDT NVARCHAR(20)
AS
BEGIN
    INSERT INTO DanhSachDuKhach (MaChuyenDi, NgayBatDau, CCCD, Ten, SDT)
    VALUES (@MaChuyenDi, @NgayBatDau, @CCCD, @Ten, @SDT);
END;
GO

CREATE PROCEDURE UpdateCustomer
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME,
    @CCCD NVARCHAR(50),
    @Ten NVARCHAR(100),
    @SDT NVARCHAR(20)
AS
BEGIN
    UPDATE DanhSachDuKhach
    SET Ten = @Ten, SDT = @SDT
    WHERE MaChuyenDi = @MaChuyenDi AND NgayBatDau = @NgayBatDau AND CCCD = @CCCD;
END;
GO

CREATE PROCEDURE sp_GetRequest
AS
BEGIN
    SELECT MaTaiKhoan, MaChuyenDi, NgayBatDau, SoLuong
    FROM YeuCau
END
GO

CREATE PROCEDURE sp_DeleteReq
    @MaTaiKhoan INT,
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME
AS
BEGIN
    DELETE FROM YeuCau
    WHERE MaTaiKhoan = @MaTaiKhoan
    AND MaChuyenDi = @MaChuyenDi
    AND NgayBatDau = @NgayBatDau;
END;
GO

CREATE PROCEDURE ThemLichTrinh
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME
AS
BEGIN
    INSERT INTO LichTrinh (MaChuyenDi, NgayBatDau)
    VALUES (@MaChuyenDi, @NgayBatDau);
END
GO

CREATE PROCEDURE sp_FilterLichTrinh
    @MaChuyenDi NVARCHAR(50) = NULL,
    @MaHDV NVARCHAR(50) = NULL,
    @SoLuong INT,
    @NotEligible BIT
AS
BEGIN
    SELECT 
        lt.MaChuyenDi,
        lt.NgayBatDau,
        ISNULL(lt.MaHDV, '') AS MaHDV,
        ISNULL(COUNT(dk.CCCD), 0) AS SoLuongHienTai,
        cd.SoLuong AS SoLuongToiDa
    FROM LichTrinh lt
    JOIN ChuyenDi cd ON lt.MaChuyenDi = cd.MaChuyenDi
    LEFT JOIN DanhSachDuKhach dk ON lt.MaChuyenDi = dk.MaChuyenDi AND lt.NgayBatDau = dk.NgayBatDau
    GROUP BY lt.MaChuyenDi, lt.NgayBatDau, lt.MaHDV, cd.SoLuong
    HAVING 
        (@MaChuyenDi IS NULL OR lt.MaChuyenDi = @MaChuyenDi) AND
        (@MaHDV IS NULL OR lt.MaHDV = @MaHDV) AND
        (
            (@NotEligible = 1 AND (ISNULL(COUNT(dk.CCCD), 0) < cd.SoLuong * 0.5 OR lt.MaHDV IS NULL)) OR
            (@NotEligible = 0 AND ISNULL(COUNT(dk.CCCD), 0) >= cd.SoLuong * 0.5)
        )
END
GO

CREATE PROCEDURE sp_GetLichTrinhWithSoLuong
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SELECT 
        lt.MaChuyenDi,
        lt.NgayBatDau,
        ISNULL(lt.MaHDV, '') AS MaHDV,
        COUNT(dk.CCCD) AS SoLuongNow,
        cd.SoLuong AS SoLuongMax
    FROM LichTrinh lt
    INNER JOIN ChuyenDi cd ON lt.MaChuyenDi = cd.MaChuyenDi
    LEFT JOIN DanhSachDuKhach dk ON lt.MaChuyenDi = dk.MaChuyenDi AND lt.NgayBatDau = dk.NgayBatDau
    WHERE lt.NgayBatDau BETWEEN @FromDate AND @ToDate
    GROUP BY lt.MaChuyenDi, lt.NgayBatDau, lt.MaHDV, cd.SoLuong
END
GO

CREATE PROCEDURE sp_GetAllGuides
AS
BEGIN
    SELECT MaHDV, Ten, SDT, Email FROM HuongDanVien
END
GO

CREATE PROCEDURE DeleteItinerary
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME
AS
BEGIN
   
    DELETE FROM DanhSachDuKhach WHERE MaChuyenDi = @MaChuyenDi AND NgayBatDau = @NgayBatDau;

    DELETE FROM DanhSachDangKy WHERE MaChuyenDi = @MaChuyenDi AND NgayBatDau = @NgayBatDau;

    DELETE FROM LichTrinh WHERE MaChuyenDi = @MaChuyenDi AND NgayBatDau = @NgayBatDau;
END
GO

CREATE PROCEDURE sp_LichTrinhHD
    @MaHDV NVARCHAR(20)
AS
BEGIN
    SELECT MaChuyenDi, NgayBatDau, MaHDV
    FROM LichTrinh
    WHERE MaHDV = @MaHDV
END
GO

CREATE PROCEDURE sp_DeleteGuide
    @MaHDV NVARCHAR(10)
AS
BEGIN
    UPDATE LichTrinh
    SET MaHDV = NULL
    WHERE MaHDV = @MaHDV;

    DELETE FROM HuongDanVien
    WHERE MaHDV = @MaHDV;
END
GO

CREATE PROCEDURE sp_ThemHuongDanVien
    @MaHDV NVARCHAR(10),
    @Ten NVARCHAR(50),
    @SDT NVARCHAR(15),
    @Email NVARCHAR(50)
AS
BEGIN
    INSERT INTO HuongDanVien (MaHDV, Ten, SDT, Email)
    VALUES (@MaHDV, @Ten, @SDT, @Email);
END
GO

CREATE PROCEDURE sp_UpdateHuongDanVien
    @MaHDV NVARCHAR(10),
    @Ten NVARCHAR(50),
    @SDT NVARCHAR(15),
    @Email NVARCHAR(50)
AS
BEGIN
    UPDATE HuongDanVien
    SET Ten = @Ten,
        SDT = @SDT,
        Email= @Email
    WHERE MaHDV = @MaHDV
END
GO

CREATE PROCEDURE sp_GetAllHDVTours
AS
BEGIN
    SELECT MaChuyenDi, NgayBatDau, MaHDV
    FROM LichTrinh
END
GO

CREATE PROCEDURE sp_UpdateLichTrinh
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME,
    @MaHDV NVARCHAR(10)
AS
BEGIN
    UPDATE LichTrinh
    SET MaHDV = @MaHDV
    WHERE MaChuyenDi = @MaChuyenDi
    AND NgayBatDau = @NgayBatDau
END
GO

CREATE PROCEDURE sp_DeleteHDV
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME
AS
BEGIN
    UPDATE LichTrinh
    SET MaHDV = NULL
    WHERE MaChuyenDi = @MaChuyenDi
    AND NgayBatDau = @NgayBatDau
END
GO

CREATE PROCEDURE InsertOrUpdateThoiTiet
    @MaChuyenDi NVARCHAR(50),
    @Ngay DATETIME,
    @DiaDiem NVARCHAR(100),
    @DuBao NVARCHAR(255),
    @TrangThai NVARCHAR(50)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM ThoiTiet WHERE MaChuyenDi = @MaChuyenDi AND Ngay = @Ngay)
    BEGIN
        UPDATE ThoiTiet
        SET 
            DuBao = @DuBao,
            TrangThai = @TrangThai
        WHERE MaChuyenDi = @MaChuyenDi AND Ngay = @Ngay AND DiaDiem = @DiaDiem
    END
    ELSE
    BEGIN
        INSERT INTO ThoiTiet (MaChuyenDi, Ngay, DiaDiem, DuBao, TrangThai)
        VALUES (@MaChuyenDi, @Ngay, @DiaDiem, @DuBao, @TrangThai)
    END
END
GO

CREATE PROCEDURE sp_GetEmailsByLichTrinh
    @MaChuyenDi NVARCHAR(50),
    @NgayBatDau DATETIME
AS
BEGIN
    SELECT DISTINCT T.Email
    FROM DanhSachDangKy DDK
    JOIN TaiKhoan TK ON DDK.MaTaiKhoan = TK.ID
    JOIN ThongTinCaNhan T ON T.MaTaiKhoan = TK.ID
    WHERE DDK.MaChuyenDi = @MaChuyenDi AND DDK.NgayBatDau = @NgayBatDau
END
