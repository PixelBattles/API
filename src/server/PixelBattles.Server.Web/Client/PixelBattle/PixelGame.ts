import { HubClient } from "./HubClient";
import { PixelCanvas } from "./PixelCanvas";
import { IApiClient } from "./IApiClient";

export class PixelGame {
    private apiClient: IApiClient;
    private hubClient: HubClient;

    private gameContainer: HTMLDivElement;
    private gameId: string;

    private pixelCanvas: PixelCanvas;
    private canvasContainer: HTMLDivElement
    
    
    
    constructor(gameContainer: HTMLDivElement, apiClient: IApiClient, gameId: string) {
        this.gameContainer = gameContainer;
        this.gameId = gameId;
        this.apiClient = apiClient;

        this.canvasContainer = this.createCanvasContainer();
        this.gameContainer.appendChild(this.canvasContainer);
        this.pixelCanvas = this.createCanvas(this.canvasContainer);

        //this.hubClient = new HubClient(hubUrl, hubToken);
    }

    private createCanvasContainer(): HTMLDivElement {
        let canvasContainer: HTMLDivElement = <HTMLDivElement>document.createElement('div');
        canvasContainer.className = "canvasContainer";
        return canvasContainer;
    }

    private createCanvas(canvasContainer: HTMLDivElement): PixelCanvas {
        let pixelCanvas: PixelCanvas = new PixelCanvas(canvasContainer);
        return pixelCanvas;
    }
}