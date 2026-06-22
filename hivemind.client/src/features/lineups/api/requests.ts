import type { Lineup, LineupItem } from '../types';
import type { EnumOptionsObj } from '@/utilities/types';

export const getLineup = async (id: number) => {
    const response = await fetch('/api/lineups/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch lineup: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
}

export const updateLineup = async (lineup: Lineup) => {
    const result = await fetch('/api/lineups/' + lineup.lineupId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...lineup }),
    });

    return result;
}

export const getLineups = async (): Promise<Lineup[]> => {
    const response = await fetch('/api/lineups');

    if (!response.ok) {
        throw new Error(`Failed to fetch lineups: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.lineups;
};

export const createLineup = async (lineup: Lineup) => {
    return await fetch('/api/lineups', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...lineup }),
    });
}

export const deleteLineup = async (id: number) => {
    return await fetch('/api/lineups/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });
}


export const createLineupItem = async(lineupItem: LineupItem) => {
    return await fetch('/api/lineupitems', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...lineupItem }),
    });
}

export const updateLineupItem = async (lineupItem: LineupItem) => {
    return await fetch('/api/lineupitems/' + lineupItem.lineupItemId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...lineupItem }),
    });
}

export const getLineupItem = async (id: number): Promise<LineupItem> => {
    const response = await fetch('/api/lineupitems/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch lineup item: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const getQueryTypes = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/api/queries/types');

    if (!response.ok) {
        throw new Error(`Failed to fetch query Types: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.types;
};

export const getPlayoutTypes = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/api/enums/playout-types');

    if (!response.ok) {
        throw new Error(`Failed to fetch playout Types: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.types;
};

export const getPadToOptions = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/api/enums/padto');

    if (!response.ok) {
        throw new Error(`Failed to get pad to options: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.options
};