import { ApiClient } from "./Clients/ApiClient";
import { IApiClient, IBattle } from "./Clients/IApiClient";
import { BattleHeader } from "./Controls/BattleHeader";
import { BattleBody } from "./Controls/BattleBody";
import { IHubClient } from "./Clients/IHubClient";
import { HubClient } from "./Clients/HubClient";
import { BattleFooter } from "./Controls/BattleFooter";

export class PixelBattle {
    private apiClient: IApiClient;
    private hubClient: IHubClient;

    private battleId: string;
    private battle: IBattle;
    private widgetContainer: HTMLDivElement;

    private header: BattleHeader;
    private footer: BattleFooter;
    private body: BattleBody;
    
    constructor(widgetContainer: HTMLDivElement) {
        this.widgetContainer = widgetContainer;
        this.battleId = this.widgetContainer.getAttribute("battle-id");

        this.initializeInternal();
    }

    private async initializeInternal(): Promise<void> {
        this.apiClient = new ApiClient("/api/");
        this.battle = await this.apiClient.getBattle(this.battleId);
        let hubToken = await this.apiClient.getBattleToken(this.battleId);
        this.hubClient = new HubClient("http://localhost:10000/hubs/battles", hubToken.token);

        this.header = this.initializeHeader(this.battle.name);
        this.footer = this.initializeFooter()
        this.body = this.initializeBody();

        window.onresize = this.resize;
        window.onload = this.resize;
        this.resize(null);
    }

    private initializeHeader(text: string): BattleHeader {
        return new BattleHeader(this.widgetContainer, text);
    }

    private initializeFooter(): BattleFooter {
        return new BattleFooter(this.widgetContainer);
    }

    private initializeBody(): BattleBody {
        return new BattleBody(this.widgetContainer, this.battle, this.hubClient, this.widgetContainer.offsetWidth, this.widgetContainer.offsetHeight);
    }

    public resize = (ev: Event): void => {
        this.body.resize(this.widgetContainer.offsetWidth, this.widgetContainer.offsetHeight);
    }
}