import { IHubClient } from "../../Clients/IHubClient";

export class Chunk {
    public canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;
    private hubClient: IHubClient;

    public xIndex: number;
    public yIndex: number;

    constructor(hubClient: IHubClient, xIndex: number, yIndex: number, width: number, height: number) {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.hubClient = hubClient;

        this.canvas = document.createElement('canvas');
        this.ctx = this.canvas.getContext("2d");
        this.canvas.width = width;
        this.canvas.height = height;

        //hubClient.getChunkState(xIndex, yIndex,)

        //random for now
        this.ctx.beginPath();
        this.ctx.rect(0, 0, this.canvas.width, this.canvas.height);
        this.ctx.fillStyle = this.getRandomColor();
        this.ctx.fill();
    }

    private getRandomColor(): string {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }
}