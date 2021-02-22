import { __decorate, __metadata } from "tslib";
import { HttpClient } from 'aurelia-http-client';
import { inject, NewInstance } from 'aurelia-dependency-injection';
import { SettingsService } from './settings-service';
let DataService = class DataService {
    constructor(client, settingService) {
        this.client = client;
        this.settingService = settingService;
    }
    SendAsset(asset) {
        return this.client.post(this.settingService.defaultApiEndpoint, asset);
    }
};
DataService = __decorate([
    inject(NewInstance.of(HttpClient), SettingsService),
    __metadata("design:paramtypes", [HttpClient, SettingsService])
], DataService);
export { DataService };
export class Asset {
}
//# sourceMappingURL=data-service.js.map