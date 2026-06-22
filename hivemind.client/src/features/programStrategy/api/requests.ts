import type { ProgramStrategy } from "../types";


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
    const result = await fetch('/api/strategies/' + strategy.programStrategyId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(strategy),
    });

    return result;
}