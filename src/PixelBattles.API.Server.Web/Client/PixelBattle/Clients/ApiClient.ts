import { IApiClient, IBattle, ITokenResult } from "./IApiClient";
import { IHubClient } from "./IHubClient";
import { DefaultHttpClient, HttpClient } from "@aspnet/signalr";
import { ApiHttpClient } from "./ApiHttpClient";

export class ApiClient implements IApiClient {
    private baseUrl: string;
    private httpClient: HttpClient;

    constructor(baseUrl: string) {
        this.baseUrl = baseUrl;
        this.httpClient = new ApiHttpClient(null);
    }

    public async getBattle(battleId: string): Promise<IBattle> {
        let result = await this.httpClient.get(this.baseUrl + "battle/" + battleId)
        return JSON.parse(result.content.toString());
    }

    public async getBattleToken(battleId: string): Promise<ITokenResult> {
        let result = await this.httpClient.post(
            this.baseUrl + "battle/" + battleId + "/token",
            {
                headers: { 'Content-Type': 'application/json; charset=utf-8'}
            });
        return JSON.parse(result.content.toString());
    }
}