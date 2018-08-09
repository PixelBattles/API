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

//    public createHubClient(gameId: string, url: string, token: string): Promise<IHubClient> {
//        return new Promise<IHubClient>((resolve, reject) => {
//            reject("Failed to resolve hub");
//            //let hubClient = new HubClient(url)
//        });
//    }

//    public getGameInfo(gameId: string): Game {
//        if (gameId == "a5239ac0-b598-40e3-8f16-dae522be6e3c") {
//            return {
//                gameId: "a5239ac0-b598-40e3-8f16-dae522be6e3c",
//                height: 1000,
//                width: 1000,
//                cooldown: 60,
//                startDateUTC: "2018-03-06T02:32:46.974Z",
//                endDateUTC: "2019-03-06T02:32:46.974Z"
//            };
//        } else {
//            return null;
//        }
//    }
}