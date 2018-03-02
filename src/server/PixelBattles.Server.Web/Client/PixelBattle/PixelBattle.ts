import { PixelGame } from "./PixelGame";

export class PixelBattle {
    private game: PixelGame;

    constructor(container: HTMLElement, battleId: string, hubUrl: string, hubToken: string) {
        this.game = new PixelGame(container, battleId, hubUrl, hubUrl);
    }
}