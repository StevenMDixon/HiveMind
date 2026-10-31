export interface ProgramStrategy {
    id: number;
    name: string;
    advancedDays: number;
    active: boolean;
    startDate?: string;
    endDate?: string;
    programStrategyItems?: ProgramStrategyLineup[];
}

export interface ProgramStrategyLineup {
    id: number,
    programStrategyId: number,
    lineUpId: number,
    selectionOption: string,
    selectionType: number
}

export interface Lineup {
    id: number;
    name: string;
}