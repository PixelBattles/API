import { HubClient } from "./HubClient";
import { PixelCanvas } from "./PixelCanvas";

export class PixelGame {
    private gameContainer: HTMLDivElement;
    private gameId: string;

    private pixelCanvas: PixelCanvas;
    private canvasContainer: HTMLDivElement
    
    private hubClient: HubClient;
    
    constructor(gameContainer: HTMLDivElement, gameId: string) {
        this.gameContainer = gameContainer;
        this.gameId = gameId;

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