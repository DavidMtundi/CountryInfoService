namespace CountryInfoService.Models.Entities;

public class Country
{
    public int Id { get; set; }
    public string ISOCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CapitalCity { get; set; } = string.Empty;
    public string PhoneCode { get; set; } = string.Empty;
    public string ContinentCode { get; set; } = string.Empty;
    public string CurrencyISOCode { get; set; } = string.Empty;
    public string CountryFlag { get; set; } = string.Empty;
}