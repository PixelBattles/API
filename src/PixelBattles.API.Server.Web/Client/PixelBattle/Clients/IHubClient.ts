export interface IHubClient {
    onConnected: Promise<void>;
    subscribeToChunk(key: IChunkKey, callback: (message: IChunkStreamMessage) => void): Promise<void>;
    unsubscribeFromChunk(key: IChunkKey): Promise<void>;
    enqueueAction(key: IChunkKey, action: IChunkAction): Promise<void>;
    processAction(key: IChunkKey, action: IChunkAction): Promise<number>;
}

export interface IChunkState {
    changeIndex: number;
    image: string;
}

export interface IChunkKey {
    x: number;
    y: number;
}

export interface IChunkAction {
    x: number;
    y: number;
    color: number;
}

export interface IChunkUpdate {
    x: number;
    y: number;
    color: number;
    changeIndex: number;
}

export interface IChunkStreamMessage {
    state: IChunkState;
    key: IChunkKey;
    update: IChunkUpdate;
}