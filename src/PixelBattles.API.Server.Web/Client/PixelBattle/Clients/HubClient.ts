import { HubConnection, LogLevel, HubConnectionBuilder, HttpTransportType } from "@aspnet/signalr"
import { IHubClient, IBattleInfo, IBattleAction, IChunkState, } from "./IHubClient";

export class HubClient implements IHubClient {
    private hubConnection: HubConnection;
    public onConnected: Promise<void>;
    private url: string;
    private token: string;

    constructor(url: string, token: string) {
        this.url = url;
        this.token = token;

        this.handleConnection();
    }

    private createConnection(): HubConnection {
        return new HubConnectionBuilder()
            .withUrl(this.url, {
                transport: HttpTransportType.WebSockets,
                logger: LogLevel.Information,
                accessTokenFactory: () => this.token,
                skipNegotiation: true
            })
            .build();
    }

    private handleConnection() {
        this.hubConnection = this.createConnection();
        this.hubConnection.onclose(function (e) {
            this.handleConnection();
        });
        this.onConnected = this.hubConnection.start().catch(function (e) {
            console.log("Error on hub connection.");
            this.handleConnection();
        });
    }
    getBattleInfo(): Promise<IBattleInfo> {
        throw new Error("Method not implemented.");
    }
    processAction(action: IBattleAction): Promise<boolean> {
        throw new Error("Method not implemented.");
    }
    getChunkState(xIndex: number, yIndex: number): Promise<IChunkState> {
        return this.hubConnection.invoke("GetChunkState", { X: xIndex, Y: yIndex });
    }
    subscribeToChunk(xChunkIndex: number, yChunkIndex: number, callback: (...args: any[]) => void): Promise<boolean> {
        throw new Error("Method not implemented.");
    }
    unsubscribeFromChunk(xChunkIndex: number, yChunkIndex: number): Promise<boolean> {
        throw new Error("Method not implemented.");
    }
}