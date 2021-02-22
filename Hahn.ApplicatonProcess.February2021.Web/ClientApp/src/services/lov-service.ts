import { HttpClient, HttpResponseMessage } from 'aurelia-http-client';
import { inject, NewInstance } from 'aurelia-dependency-injection';
import { SettingsService } from './settings-service';

@inject(NewInstance.of(HttpClient), SettingsService)

export class LovService {
    get departments(): Map<String, String> {
        let text = localStorage.getItem(this.settingService.storageKeyDepartments);
        if (text) {
            let raw = JSON.parse(text).departments as Array<any>;
            let result = new Map<String, String>();
            raw.forEach((x) => result.set(x.id, x.departmentName));
            return result;
        }
        return new Map<String, String>();
    }

    get countries(): Map<String, String> {
        let text = localStorage.getItem(this.settingService.storageKeyCountries);
        if (text) {
            let raw = JSON.parse(text).countries as Array<any>;
            let result = new Map<String, String>();
            raw.forEach((x) => result.set(x.alpha3Code, x.translations[this.settingService.defaultLocale]));
            return result;
        }
        return new Map<String, String>();
    }

    get domains(): Array<String> {
        let text = localStorage.getItem(this.settingService.storageKeyTopLevelDomains);
        if (text) {
            let raw = JSON.parse(text).topLevelDomains as Array<string> ;
            return raw;
        }
        return new Array<String>();
    }

    populateDepartments(): Promise<HttpResponseMessage> {
        return this.client.get(this.settingService.defaultLovApiDepartmentsEndpoint);
    }

    populateCountries(): Promise<HttpResponseMessage> {
        return this.client.get(this.settingService.defaultLovApiCountriesEndpoint);
    }

    populateTopLevelDomains(): Promise<HttpResponseMessage> {
        return this.client.get(this.settingService.defaultLovApiTopLevelDomainsEndpoint);
    }

    constructor(private client: HttpClient,
        private settingService: SettingsService) {
    }
}