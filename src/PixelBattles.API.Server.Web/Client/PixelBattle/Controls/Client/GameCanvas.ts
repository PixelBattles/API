export class GameCanvas implements IGameCanvas {
    public canvas: HTMLCanvasElement;
    public ctx: CanvasRenderingContext2D;

    public onMoveEnd: (ev: CameraMoveEvent) => any;
    public onMoveStart: (ev: CameraMoveEvent) => any;
    public onMove: (ev: CameraMoveEvent) => any;
    public onRender: () => void;

    public constructor(canvas: HTMLCanvasElement) {
        this.canvas = canvas;
        this.ctx = this.canvas.getContext("2d");
        
        this.canvas.onmousedown = this.onmousedown;
        this.canvas.onmouseup = this.onmouseup;
        this.canvas.onmousemove = this.onmousemove;
    }

    public resize(width: number, height: number): void {
        this.canvas.width = width;
        this.canvas.height = height;
        this.onRender();
    }

    private onmousedown = (ev: MouseEvent): void => {
        this.onMoveStart(new CameraMoveEvent(ev.clientX, ev.clientY));
    }

    private onmouseup = (ev: MouseEvent): void => {
        this.onMoveEnd(new CameraMoveEvent(ev.clientX, ev.clientY));
    }

    private onmousemove = (ev: MouseEvent): void => {
        this.onMove(new CameraMoveEvent(ev.clientX, ev.clientY));
    }
}

export interface IGameCanvas {
    canvas: HTMLCanvasElement;
    ctx: CanvasRenderingContext2D;

    onMoveEnd: (ev: CameraMoveEvent) => any;
    onMoveStart: (ev: CameraMoveEvent) => any;
    onMove: (ev: CameraMoveEvent) => any;

    onRender: () => void;

    resize(width: number, height: number): void;
}

export class CameraMoveEvent {
    public constructor(x: number, y: number) {
        this.x = x;
        this.y = y;
    }
    readonly x: number;
    readonly y: number;
}