using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer;
using Newtonsoft.Json;

public class WeatherBLL
{
    WeatherDAL dal = new WeatherDAL();
    private readonly string apiKey = "e179c3ce4ad9b85da0ed8d7d2178167e";

    public void LuuThoiTietTuAPI(WeatherDTO dto)
    {
        try
        {
            dal.ThemThoiTiet(dto);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<WeatherDTO> LayTatCaThoiTiet()
    {
        return dal.LayTatCa();
    }

    public string GetDiaDiemFromTenChuyenDi(string tenChuyenDi)
    {
        if (tenChuyenDi.StartsWith("Tour "))
        {
            string diaDiem = tenChuyenDi.Substring(5);
            return diaDiem;
        }
        return string.Empty;
    }

    public async Task<(double lat, double lon)> GetCoordinatesFromDiaDiem(string diaDiem)
    {
        string url = $"http://api.openweathermap.org/geo/1.0/direct?q={diaDiem}&limit=1&appid={apiKey}";
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    if (data.Count > 0)
                    {
                        double lat = data[0].lat;
                        double lon = data[0].lon;
                        return (lat, lon);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return (0, 0);
    }

    public async Task<(string duBao, string trangThai)> GetWeatherForecast(double lat, double lon, DateTime ngayDi)
    {
        string url = $"http://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={apiKey}&units=metric";
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    string forecastDate = ngayDi.ToString("yyyy-MM-dd");
                    foreach (var item in data.list)
                    {
                        string dt = item.dt_txt;
                        if (dt.StartsWith(forecastDate))
                        {
                            double temp = item.main.temp;
                            string description = item.weather[0].description;
                            string duBao = $"Nhiệt độ: {temp}°C, {description}";
                            string trangThai = EvaluateWeather(description);
                            return (duBao, trangThai);
                        }
                    }
                }
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ("Không có dự báo cho ngày này (ngoài phạm vi 5 ngày)", "Không xác định");
    }

    private string EvaluateWeather(string description)
    {
        description = description.ToLower();

        if (description.Contains("thunderstorm") ||
            description.Contains("heavy rain") ||
            description.Contains("storm") ||
            description.Contains("snow") ||
            description.Contains("hail"))
        {
            return "Xấu";
        }
        else if (description.Contains("light rain") ||
                 description.Contains("drizzle") ||
                 description.Contains("cloud") ||
                 description.Contains("mist") ||
                 description.Contains("fog") ||
                 description.Contains("haze") ||
                 description.Contains("smoke") ||
                 description.Contains("overcast") ||
                 description.Contains("shower rain"))
        {
            return "Bình thường";
        }
        else if (description.Contains("clear") ||
                 description.Contains("sunny"))
        {
            return "Tốt";
        }

        return "Không xác định";
    }


    public async Task UpdateThoiTietForAllChuyenDi()
    {
        try
        {
            var chuyenDiList = dal.LayTatCaChuyenDi();
            if (chuyenDiList.Count == 0)
            {
                return;
            }

            foreach (var chuyenDi in chuyenDiList)
            {
                string diaDiem = GetDiaDiemFromTenChuyenDi(chuyenDi.TenChuyenDi);
                if (!string.IsNullOrEmpty(diaDiem))
                {
                    var (lat, lon) = await GetCoordinatesFromDiaDiem(diaDiem);
                    if (lat != 0 && lon != 0)
                    {
                        var ngayDiList = dal.LayNgayDiCuaChuyenDi(chuyenDi.MaChuyenDi);
                        if (ngayDiList.Count == 0)
                        {
                            continue;
                        }

                        foreach (var ngayDi in ngayDiList)
                        {
                            var (duBao, trangThai) = await GetWeatherForecast(lat, lon, ngayDi);
                            WeatherDTO thoiTiet = new WeatherDTO
                            {
                                MaChuyenDi = chuyenDi.MaChuyenDi,
                                Ngay = ngayDi,
                                DiaDiem = diaDiem,
                                DuBao = duBao,
                                TrangThai = trangThai
                            };
                            LuuThoiTietTuAPI(thoiTiet);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}