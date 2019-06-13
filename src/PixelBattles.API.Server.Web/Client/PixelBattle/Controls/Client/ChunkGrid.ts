import { Chunk, IChunk } from "./Chunk";
import { IBattle } from "../../Clients/IApiClient";
import { IHubClient } from "../../Clients/IHubClient";

export class ChunkGrid implements IChunkGrid {
    private storage: Map<number, Map<number, Chunk>>;
    private battle: IBattle;
    private hubClient: IHubClient;

    public chunkWidth: number;
    public chunkHeight: number;
    public onUpdated: () => void;

    public constructor(battle: IBattle, hubClient: IHubClient) {
        this.battle = battle;
        this.hubClient = hubClient;
        this.chunkWidth = this.battle.settings.chunkWidth;
        this.chunkHeight = this.battle.settings.chunkHeight;
        this.storage = new Map<number, Map<number, Chunk>>();
    }

    public getChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): Chunk[] {
        let resultChunks = new Array<Chunk>();
        for (var x = Math.max(this.battle.settings.minWidthIndex, xIndexFrom); x <= Math.min(this.battle.settings.maxWidthIndex, xIndexTo); x++) {
            for (var y = Math.max(this.battle.settings.minHeightIndex, yIndexFrom); y <= Math.min(this.battle.settings.maxHeightIndex, yIndexTo); y++) {
                resultChunks.push(this.getOrAddChunkInternal(x, y));
            }
        }
        return resultChunks;
    }

    public clearChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): void {
        let deleteCounter = 0;
        for (let [x, columnChunks] of this.storage) {
            if (x < xIndexFrom && x > xIndexTo) {
                for (let [y, chunk] of columnChunks) {
                    chunk.dispose();
                    deleteCounter++;
                }
                this.storage.delete(x);
            } else {
                for (let [y, chunk] of columnChunks) {
                    if (y < yIndexFrom || y > yIndexTo) {
                        chunk.dispose();
                        columnChunks.delete(y);
                        deleteCounter++;
                    }
                }
            }
        }
        if (deleteCounter) {
            console.log(`${deleteCounter} chunks was disposed.`);
        }
    }
    
    public getChunk(x: number, y: number): Chunk {
        
        if (x > this.battle.settings.maxWidthIndex || x < this.battle.settings.minWidthIndex) {
            return;
        }
        if (y > this.battle.settings.maxHeightIndex || y < this.battle.settings.minHeightIndex) {
            return;
        }
        return this.getOrAddChunkInternal(x, y);
    }

    private getOrAddChunkInternal(x: number, y: number): Chunk {
        let column = this.storage.get(x);
        if (!column) {
            column = new Map<number, Chunk>();
            this.storage.set(x, column);
        }
        let chunk = column.get(y);
        if (chunk) {
            return chunk;
        }
        else {
            let newChunk = new Chunk(this.hubClient, x, y, this.battle.settings.chunkWidth, this.battle.settings.chunkHeight, this.onChunkUpdated);
            column.set(y, newChunk);
            return newChunk;
        }
    }

    private onChunkUpdated = (chunk: IChunk) => this.onUpdated();
}
export interface IChunkGrid {
    getChunk(x: number, y: number): Chunk;
    getChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): Chunk[];
    clearChunks(xIndexFrom: number, xIndexTo: number, yIndexFrom: number, yIndexTo: number): void;
    onUpdated: () => void;
    chunkWidth: number;
    chunkHeight: number;
}