export class PixelCanvas {
    private canvasContainer: HTMLDivElement;

    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;
    private width: number;
    private height: number;

    private x: number;
    private y: number;
    private scale: number;

    constructor(canvasContainer: HTMLDivElement) {
        this.canvasContainer = canvasContainer;

        this.canvas = document.createElement('canvas');
        this.ctx = this.canvas.getContext("2d");
        this.canvasContainer.appendChild<HTMLCanvasElement>(this.canvas);
        
        this.ctx = this.canvas.getContext("2d");
        
        this.init();
    }
    
    private init(): void {
        this.x = 0;
        this.y = 0;
        this.scale = 16;
    }
        
    private render(): void {
        this.ctx.beginPath();
        this.ctx.rect(0, 0, this.canvas.width, this.canvas.height);
        this.ctx.fillStyle = "black";
        this.ctx.fill();
    }

    public resize(width: number, height: number) {
        this.canvas.width = width;
        this.canvas.height = height;
        this.render();
    }
}