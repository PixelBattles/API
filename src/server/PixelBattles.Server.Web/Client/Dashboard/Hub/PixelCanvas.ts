export class PixelCanvas {
    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;
    private width: number;
    private height: number;

    constructor(canvas: HTMLCanvasElement) {
        this.canvas = canvas;
        this.width = canvas.width;
        this.height = canvas.height
        this.ctx = canvas.getContext("2d");
    }

    public drawPixel(x: number, y: number, color: string): void {
        return;
    }

    public drawImage(image: HTMLImageElement): void {
        this.ctx.drawImage(image, 0, 0);
    }
}