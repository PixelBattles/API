import { IGameCanvas, PositionEvent } from "./GameCanvas";

export class Camera implements ICamera {
    public onRender: () => void;
    public onClick: (ev: PositionEvent) => void;

    private canvas: IGameCanvas;

    private initialMouseX: number;
    private initialMouseY: number;
    private internalCameraX: number;
    private internalCameraY: number;
    private cameraOffsetX: number = 0;
    private cameraOffsetY: number = 0;
    private internalScale: number = 8;
    private isDrag: boolean;

    public get cameraX(): number {
        return this.internalCameraX + this.cameraOffsetX;
    }
    public get cameraY(): number {
        return this.internalCameraY + this.cameraOffsetY;
    }

    public get cameraCenterX(): number {
        return this.internalCameraX + this.cameraOffsetX + (this.canvas.canvas.width / 2);
    }
    public get cameraCenterY(): number {
        return this.internalCameraY + this.cameraOffsetY + (this.canvas.canvas.height / 2);
    }

    public set scale(value: number) {
        this.internalScale = value;
        this.onRender();
    }

    public get scale(): number {
        return this.internalScale;
    }

    public constructor(canvas: IGameCanvas, width: number, height: number) {
        this.canvas = canvas;
        this.canvas.onMoveEnd = this.onMoveEnd;
        this.canvas.onMoveStart = this.onMoveStart;
        this.canvas.onMove = this.onMove;

        this.internalCameraX = 0/*center*/ - Math.floor(width / 2);
        this.internalCameraY = 0/*center*/ - Math.floor(height / 2);
    }

    private onMove = (ev: PositionEvent): void => {
        if (!this.isDrag) {
            return;
        }

        this.cameraOffsetX = this.initialMouseX - ev.x;
        this.cameraOffsetY = this.initialMouseY - ev.y;
        this.onRender();
    }

    private onMoveStart = (ev: PositionEvent): void => {
        this.isDrag = true;
        this.initialMouseX = ev.x;
        this.initialMouseY = ev.y;
        this.cameraOffsetX = 0;
        this.cameraOffsetY = 0;
    }

    private onMoveEnd = (ev: PositionEvent): void => {
        if (!this.isDrag) {
            return;
        }

        this.isDrag = false;
        this.cameraOffsetX = this.initialMouseX - ev.x;
        this.cameraOffsetY = this.initialMouseY - ev.y;
        if (this.cameraOffsetX == 0 && this.cameraOffsetY == 0) {
            let position = new PositionEvent(this.internalCameraX + ev.x, this.internalCameraY + ev.y);
            this.onClick(position);
        } else {
            this.internalCameraX = this.internalCameraX + this.cameraOffsetX;
            this.internalCameraY = this.internalCameraY + this.cameraOffsetY;
            this.cameraOffsetX = 0;
            this.cameraOffsetY = 0;
        }
        this.onRender();
    }
}

export interface ICamera {
    onRender: () => void;
    onClick: (ev: PositionEvent) => void;

    cameraX: number;
    cameraY: number;
    cameraCenterX: number;
    cameraCenterY: number;
    scale: number;
}
