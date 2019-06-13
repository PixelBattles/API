import { HubConnection, LogLevel, HubConnectionBuilder, HttpTransportType } from "@aspnet/signalr"
import { IHubClient, IChunkState, IChunkKey, IChunkStreamMessage, IChunkAction, } from "./IHubClient";

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

            this.hubConnection.stream<IChunkStreamMessage>("SubscribeToChunkStream").subscribe({
                next: (value: IChunkStreamMessage) => {
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

    subscribeToChunk(key: IChunkKey, callback: (message: IChunkStreamMessage) => void): Promise<void> {
        this.subscriptions.set(`${key.x}:${key.y}`, callback);
        return this.hubConnection.send("SubscribeToChunk", key);
    }

    unsubscribeFromChunk(key: IChunkKey): Promise<void> {
        this.subscriptions.delete(`${key.x}:${key.y}`);
        return this.hubConnection.send("UnsubscribeToChunk", key);
    }

    enqueueAction(key: IChunkKey, action: IChunkAction): Promise<void> {
        console.log(`Enqueue action ${action.x}:${action.y} with color ${action.color} on ${key.x}:${key.y}`);
        return this.hubConnection.send("EnqueueAction", key, action);
    }

    processAction(key: IChunkKey, action: IChunkAction): Promise<number> {
        console.log(`Process action ${action.x}:${action.y} with color ${action.color} on ${key.x}:${key.y}`);
        return this.hubConnection.invoke("ProcessAction", key, action);
    }
}