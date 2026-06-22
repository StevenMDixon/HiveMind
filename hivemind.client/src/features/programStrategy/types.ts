export interface ProgramStrategy {
    programStrategyId: number;
    name: string;
    advancedDays: number;
    active: boolean;
    startDate?: string;
    endDate?: string;
    lineups?: Lineup[];
}

export interface Lineup {
    lineupId: number;
    lineupName: string;
}