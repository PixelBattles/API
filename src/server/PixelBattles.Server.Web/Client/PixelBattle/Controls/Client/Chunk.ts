export class Chunk {
    public canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;

    public xIndex: number;
    public yIndex: number;

    constructor(color: string, xIndex: number, yIndex: number) {
        this.xIndex = xIndex;
        this.yIndex = yIndex;

        this.canvas = document.createElement('canvas');
        this.ctx = this.canvas.getContext("2d");
        this.canvas.width = 100;
        this.canvas.height = 100;

        this.ctx.beginPath();
        this.ctx.rect(0, 0, this.canvas.width, this.canvas.height);
        this.ctx.fillStyle = color;
        this.ctx.fill();
    }
}