import { IApiClient, Battle, Game } from "./IApiClient";
import { IHubClient } from "./IHubClient";
import { HubClient } from "./HubClient";

export class ApiClient implements IApiClient {
    constructor() {

    }

    public getBattleInfo(battleId: string): Promise<Battle> {
        return new Promise<Battle>((resolve, reject) => {
            if (battleId == "4e3c87ea-46ea-42d3-a253-a9a9524ce55d") {
                resolve({
                    battleId: "4e3c87ea-46ea-42d3-a253-a9a9524ce55d",
                    name: "Mock battle",
                    description: "Mock battle description"
                });
            } else {
                reject("Battle not found");
            }
        });
    }

    public createHubClient(gameId: string, url: string, token: string): Promise<IHubClient> {
        return new Promise<IHubClient>((resolve, reject) => {
            reject("Failed to resolve hub");
            //let hubClient = new HubClient(url)
        });
    }

    public getGameInfo(gameId: string): Game {
        if (gameId == "a5239ac0-b598-40e3-8f16-dae522be6e3c") {
            return {
                gameId: "a5239ac0-b598-40e3-8f16-dae522be6e3c",
                height: 1000,
                width: 1000,
                cooldown: 60,
                startDateUTC: "2018-03-06T02:32:46.974Z",
                endDateUTC: "2019-03-06T02:32:46.974Z"
            };
        } else {
            return null;
        }
    }
}