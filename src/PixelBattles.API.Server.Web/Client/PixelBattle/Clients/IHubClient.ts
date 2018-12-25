export interface IHubClient {
    onConnected: Promise<void>;
    getBattleInfo(): Promise<IBattleInfo>;
    processAction(action: IBattleAction): Promise<boolean>;
    getChunkState(xIndex: number, yIndex: number): Promise<IChunkState>;
    subscribeToChunk(xChunkIndex: number, yChunkIndex: number, callback: (...args: any[]) => void): Promise<boolean>;
    unsubscribeFromChunk(xChunkIndex: number, yChunkIndex: number): Promise<boolean>;
}

export interface IBattleAction {
    xChunkIndex: number;
    yChunkIndex: number;
    xIndex: number;
    yIndex: number;
    color: string;
}

export interface IBattleInfo {
}

export interface IChunkState {
    changeIndex: number;
    image: any;
}