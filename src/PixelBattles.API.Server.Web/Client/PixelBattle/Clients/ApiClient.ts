import { IApiClient, IBattle, ITokenResult } from "./IApiClient";
import { IHubClient } from "./IHubClient";
import { HttpClient } from "@aspnet/signalr-client";

export class ApiClient implements IApiClient {
    private baseUrl: string;
    private httpClient: HttpClient;

    constructor(baseUrl: string) {
        this.baseUrl = baseUrl;
        this.httpClient = new HttpClient();
    }

    public async getBattle(battleId: string): Promise<IBattle> {
        let result = await this.httpClient.get(this.baseUrl + "battle/" + battleId)
        return JSON.parse(result);
    }

    public async getBattleToken(battleId: string): Promise<ITokenResult> {
        let headers = new Map<string, string>();
        headers.set('Content-Type', 'application/json; charset=utf-8');
        let result = await this.httpClient.post(
            this.baseUrl + "battle/token",
            JSON.stringify({ battleId: battleId }),
            headers);
        return JSON.parse(result);
    }
}