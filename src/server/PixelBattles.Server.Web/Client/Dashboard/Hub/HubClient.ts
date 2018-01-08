import { HubConnection, TransportType, ConsoleLogger, LogLevel } from "@aspnet/signalr-client"

export class ClientHub {
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
        this.hubConnection.on('ActionMessage', this.onActionMessage);
        this.hubConnection.start().catch(function (e) {
            this.hubConnection();
        });
    }

    private onActionMessage(message: any): void {
    }
}