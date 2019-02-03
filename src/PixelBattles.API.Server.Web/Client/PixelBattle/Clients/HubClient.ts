import { HubConnection, LogLevel, HubConnectionBuilder, HttpTransportType } from "@aspnet/signalr"
import { IHubClient, IChunkState, IChunkKey, IChunkStreamMessage, } from "./IHubClient";

export class HubClient implements IHubClient {    
    private hubConnection: HubConnection;
    public onConnected: Promise<void>;
    private subscriptions: Map<string, (message: IChunkStreamMessage) => void>;
    private url: string;
    private token: string;

    constructor(url: string, token: string) {
        this.url = url;
        this.token = token;
        this.subscriptions = new Map<string, (message: IChunkStreamMessage) => void>();

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
        this.onConnected = this.hubConnection.start().then(() => {
            console.log("Hub connected.");
            window.customAction = (x: number, y: number, cx: number, cy: number, color: number) => {
                var changeIndex = this.hubConnection.invoke("Process", { x: x, y: y }, { x: cx, y: cy, color: color });
                console.log(changeIndex);
            }

            this.hubConnection.stream<IChunkStreamMessage>("SubscribeToChunkStream").subscribe({
                next: (value: IChunkStreamMessage) => {
                    console.log("Next on hub connection.");
                    var callback = this.subscriptions.get(`${value.key.x}:${value.key.y}`);
                    if (callback) {
                        callback(value);
                    }
                },
                complete: () => {
                    console.log("Complete on hub connection.");
                },
                error: (err) => {
                    console.log("Error on hub connection.");
                },
            });
        }, function (e) {
            console.log("Error on hub connection.");
            this.handleConnection();
        });
    }

    subscribeToChunk(key: IChunkKey, callback: (message: IChunkStreamMessage) => void): void {
        console.log(`Subs on ${key.x}:${key.y}`);
        this.subscriptions.set(`${key.x}:${key.y}`, callback);
        this.hubConnection.send("SubscribeToChunk", key);
    }
    unsubscribeFromChunk(key: IChunkKey): void {
        console.log(`Unsubs on ${key.x}:${key.y}`);
        this.subscriptions.delete(`${key.x}:${key.y}`);
        this.hubConnection.send("UnsubscribeToChunk", key);
    }
}