import { HttpClient, HttpResponseMessage } from 'aurelia-http-client';
import { inject, NewInstance } from 'aurelia-dependency-injection';
import { SettingsService } from './settings-service';

@inject(NewInstance.of(HttpClient), SettingsService)
export class DataService {

    constructor(private client: HttpClient, private settingService:SettingsService) {

    }

    public SendAsset(asset: Asset): Promise<HttpResponseMessage> {
        return this.client.post(this.settingService.defaultAssetApiEndpoint, asset);
    }
}

export class Asset {
    public assetName: string;
    public department: string;
    public country: string;
    public purchaseDate: Date;
    public email: string;
    public broken: string;
}