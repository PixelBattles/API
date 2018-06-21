import { HubConnection, TransportType, ConsoleLogger, LogLevel } from "@aspnet/signalr-client"
import { IHubClient, } from "./IHubClient";

export class HubClient implements IHubClient {
    //connectToGame(gameId: string): Promise<boolean> {
    //    throw new Error("Method not implemented.");
    //}
    //disconnectFromGame(gameId: string): Promise<boolean> {
    //    throw new Error("Method not implemented.");
    //}
    //getGameState(gameId: string): Promise<IGameState> {
    //    throw new Error("Method not implemented.");
    //}
    //processAction(action: IBattleAction): Promise<IActionResult> {
    //    throw new Error("Method not implemented.");
    //}
    //getGameDelta(gameId: string, from: number, to: number): Promise<IDeltaResult> {
    //    throw new Error("Method not implemented.");
    //}

    private hubConnection: HubConnection;
    private url: string;
    private token: string;

    constructor(url: string, token: string) {
        this.url = url;
        this.token = token;

        this.handleConnection();
    }

    private createConnection() {
        return new HubConnection(this.url + '?token=' + this.token, { transport: TransportType.WebSockets, logging: new ConsoleLogger(LogLevel.Information) });
    }

    private handleConnection() {
        this.hubConnection = this.createConnection();
        this.hubConnection.onclose(function (e) {
            this.handleConnection();
        });
        //this.hubConnection.on('ActionMessage', this.onActionMessage);
        this.hubConnection.start().catch(function (e) {
            this.hubConnection();
        });
    }

    //private onActionMessage(): void {

    //}
}