import { IHubClient } from "../../Clients/IHubClient";

export class Chunk implements IChunk {
    private ctx: CanvasRenderingContext2D;
    private hubClient: IHubClient;

    public xIndex: number;
    public yIndex: number;
    public changeIndex: number;
    public isVisible: boolean;
    public canvas: HTMLCanvasElement;
    public onUpdated: (chunk: IChunk) => void;

    constructor(hubClient: IHubClient, xIndex: number, yIndex: number, width: number, height: number, onUpdated: (chunk: IChunk) => void) {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.onUpdated = onUpdated;
        this.hubClient = hubClient;
        
        this.canvas = document.createElement('canvas');
        this.ctx = this.canvas.getContext("2d");
        this.canvas.width = width;
        this.canvas.height = height;
        
        this.hubClient.onConnected.then(() => {
            hubClient.subscribeToChunk({ x: xIndex, y: yIndex }, message => {
                if (message.state) {
                    let image = new Image();
                    image.onload = () => {
                        this.ctx.drawImage(image, 0, 0);
                    };
                    image.src = "data:image/png;base64," + message.state.image;
                    this.changeIndex = message.state.changeIndex;
                    this.onUpdated(this);
                }
            });
        });
    }
}

export interface IChunk {
    xIndex: number;
    yIndex: number;
    changeIndex: number;
    canvas: HTMLCanvasElement;
    onUpdated: (chunk: IChunk) => void;
}