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
            hubClient.getChunkState(xIndex, yIndex).then(state => {
                let image = new Image();
                image.onload = () => {
                    this.ctx.drawImage(image, 0, 0);
                };
                image.src = "data:image/png;base64," + state.image;
                this.changeIndex = state.changeIndex;
                this.onUpdated(this);
            }, error => {
                console.log(error);
                this.ctx.beginPath();
                this.ctx.rect(0, 0, this.canvas.width, this.canvas.height);
                this.ctx.fillStyle = "grey";
                this.ctx.fill();
                this.onUpdated(this);
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