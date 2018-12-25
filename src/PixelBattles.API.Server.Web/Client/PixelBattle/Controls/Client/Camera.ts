import { IGameCanvas, CameraMoveEvent } from "./GameCanvas";

export class Camera implements ICamera {
    public onRender: () => void;
    private canvas: IGameCanvas;
    
    private initialMouseX: number;
    private initialMouseY: number;
    private internalCameraX: number;
    private internalCameraY: number;
    private cameraOffsetX: number = 0;
    private cameraOffsetY: number = 0;
    private isDrag: boolean;

    public get cameraX(): number {
        return this.internalCameraX + this.cameraOffsetX;
    }
    public get cameraY(): number {
        return this.internalCameraY + this.cameraOffsetY;
    }
    public scale: number;
    
    public constructor(canvas: IGameCanvas) {
        this.canvas = canvas;
        this.canvas.onMoveEnd = this.onMoveEnd;
        this.canvas.onMoveStart = this.onMoveStart;
        this.canvas.onMove = this.onMove;

        this.internalCameraX = 0;
        this.internalCameraY = 0;
        this.scale = 1;
    }

    private onMove = (ev: CameraMoveEvent): void => {
        if (this.isDrag) {
            this.cameraOffsetX = ev.x - this.initialMouseX;
            this.cameraOffsetY = ev.y - this.initialMouseY;
            this.onRender();
        }
    }

    private onMoveStart = (ev: CameraMoveEvent): void => {
        this.isDrag = true;
        this.initialMouseX = ev.x;
        this.initialMouseY = ev.y;
        this.cameraOffsetX = 0;
        this.cameraOffsetY = 0;
        this.onRender();
    }

    private onMoveEnd = (ev: CameraMoveEvent): void => {
        this.isDrag = false;
        this.cameraOffsetX = ev.x - this.initialMouseX;
        this.cameraOffsetY = ev.y - this.initialMouseY;
        this.internalCameraX = this.internalCameraX + this.cameraOffsetX;
        this.internalCameraY = this.internalCameraY + this.cameraOffsetY;
        this.cameraOffsetX = 0;
        this.cameraOffsetY = 0;
        this.onRender();
    }
}

export interface ICamera {
    onRender: () => void;

    cameraX: number;
    cameraY: number;
    scale: number;
}
