import { HubClient } from "./HubClient";
import { PixelCanvas } from "./PixelCanvas";

export class PixelGame {
    private container: HTMLElement;
    private hubClient: HubClient;
    private pixelCanvas: PixelCanvas;

    constructor(container: HTMLElement, battleId: string, hubUrl: string, hubToken: string) {
        this.container = container;

        let canvasElement = document.createElement('canvas');
        canvasElement.id = battleId;
        canvasElement.height = 1000;
        canvasElement.width = 1000;
        this.container.appendChild<HTMLCanvasElement>(canvasElement);

        this.hubClient = new HubClient(hubUrl, hubToken);
        this.pixelCanvas = new PixelCanvas(canvasElement);
    }
}