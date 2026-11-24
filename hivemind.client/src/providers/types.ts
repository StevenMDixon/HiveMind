export enum ImportState {
    Idle = 0,
    Running = 1,
    Failed = 2,
    Completed = 3
}

export interface ImportStatus {
    itemId: string | null;
    itemName: string | null;
    state: ImportState;
    message: string | null;
    progress: number | null;
    updatedAt: string;
}