export class SettingsService {
    public defaultLocale: string = "en";
    public defaultDateFormat: string = "MM/DD/YYYY";
    public defaultAssetApiEndpoint: string = "/api/asset";
    public defaultLovApiDepartmentsEndpoint: string = "/api/listofvalues/getdepartments";
    public defaultLovApiCountriesEndpoint: string = "/api/listofvalues/getcountries";
    public defaultLovApiTopLevelDomainsEndpoint: string = "/api/listofvalues/gettopleveldomains";
    public storageKeyDepartments: string = "Departments";
    public storageKeyCountries: string = "Countries";
    public storageKeyTopLevelDomains: string = "TopLevelDomains";

}