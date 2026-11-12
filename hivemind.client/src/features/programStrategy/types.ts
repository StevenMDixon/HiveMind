export interface ProgramStrategy {
    id: number;
    name: string;
    advancedDays: number;
    active: boolean;
    startDate?: string;
    endDate?: string;
    programStrategyItems?: ProgramStrategyLineup[];
    programStrategyEvents?: ProgramStrategyEvent[];
}

export interface ProgramStrategyLineup {
    id: number,
    programStrategyId: number,
    lineUpId: number,
    selectionOption: string,
    selectionType: number
}

export interface ProgramStrategyEvent {
    id: number,
    programStrategyId: number,
    name: string,
    queryId: number,
    promoDays: number,
    eventDate: Date
}

export interface Lineup {
    id: number;
    name: string;
}

export interface Query {
    id: number;
    name: string;
}