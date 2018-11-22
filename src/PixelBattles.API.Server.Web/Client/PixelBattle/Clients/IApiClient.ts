export interface IApiClient {
    getBattle(battleId: string): Promise<IBattle>; 
    getBattleToken(battleId: string): Promise<ITokenResult>; 
}

export interface IBattle {
    battleId: string;
    name: string;
    description: string;
    settings: {
        chunkHeight: number;
        chunkWidth: number;
        maxHeightIndex: number;
        maxWidthIndex: number;
        minHeightIndex: number;
        minWidthIndex: number;
        centerX: number;
        centerY: number;
        cooldown: number;
    };
    startDateUTC: Date;
    endDateUTC: Date;
}

export interface ITokenResult extends IResult {
    token: string;
}

export interface IResult {
    succeeded: boolean;
    errors: Error;
}

export interface IError {
    Code: string;
    Description: string;
}