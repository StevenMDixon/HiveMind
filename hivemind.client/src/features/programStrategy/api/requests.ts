import type { EnumOptionsObj } from "../../../components/FormFields";
import type PaginatedDataResponse from "../../../utilities/PaginatedData";
import type { ProgramStrategy, Lineup, Query } from "../types";



export const getProgramStrategy = async (strategyId: number): Promise<ProgramStrategy> => {
    const response = await fetch('/api/strategies/' + strategyId);

    if (!response.ok) {
        throw new Error(`Failed to fetch strategy: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const getProgramStrategies = async () => {
    const response = await fetch('/api/strategies');
    if (response.ok) {
        const data = await response.json();
        return data.strategies;
    }
};

export const createProgramStrategy = async (strategy: ProgramStrategy) => {
    const response = await fetch('/api/strategies', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(strategy),
    });

    return response;
};

export const deleteProgramStrategy = async (strategyId: number) => {
    const response = await fetch(`/api/strategies/${strategyId}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });

    return response;
};

export const updateProgramStrategy = async (strategy: ProgramStrategy) => {

    const result = await fetch('/api/strategies/' + strategy.id, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(strategy),
    });

    return result;
}

export const getLineupSelectionTypes = async () => {
    const response = await fetch('api/enums/lineupselection')

    if (response.ok) {
        const data = await response.json();
        return data.options;
    }
}

export const getLineups = async (): Promise<Lineup[]> => {
    const response = await fetch('/api/lineups');

    if (!response.ok) {
        throw new Error(`Failed to fetch lineups: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.lineups;
};

export const getQueries = async (): Promise<Query[]> => {
    const response = await fetch('/api/queries');

    if (!response.ok) {
        throw new Error(`Failed to fetch queries: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.queries.reduce((acc: EnumOptionsObj, cur: Query) => { acc[cur.id] = cur.name; return acc; }, {} as Query);;
};

export const search = async (searchTerm: string, page: number, count: number): Promise<PaginatedDataResponse<ProgramStrategy>> => {
    let url = "/api/strategies";

    url += `?search=${searchTerm}&count=${count}&page=${page}`;

    const response = await fetch(url);

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return {data: data.strategies, dataCount: data.dataCount} as PaginatedDataResponse<ProgramStrategy>;
};