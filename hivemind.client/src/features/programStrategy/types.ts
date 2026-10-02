export interface ProgramStrategy {
    programStrategyId: number;
    name: string;
    advancedDays: number;
    active: boolean;
    startDate?: string;
    endDate?: string;
    programStrategyItems?: ProgramStrategyLineup[];
}

export interface ProgramStrategyLineup {
    programStrategyLineupId: number,
    programStrategyId: number,
    lineUpId: number,
    selectionOption: string,
    selectionType: number
}

export interface Lineup {
    lineupId: number;
    lineupName: string;
}