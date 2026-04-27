import type { QueryLineupItem, Lineup, LineupItem } from '../Types/Lineup';

export const fetchLineup = async (id: number) => {
    const response = await fetch('/lineups/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch lineup: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
}

export const updateLineup = async (lineup: Lineup) => {
    const result = await fetch('/lineups/' + lineup.lineupId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...lineup }),
    });

    return result;
}

export const fetchLineups = async (): Promise<Lineup[]> => {
    const response = await fetch('/lineups');

    if (!response.ok) {
        throw new Error(`Failed to fetch lineups: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.lineups;
};

export const createLineup = async (lineup: Lineup) => {
    return await fetch('/lineups', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ LineupName: lineup.lineupName, ChannelId: lineup.channelId }),
    });
}

export const deleteLineup = async (lineup: Lineup) => {
    return await fetch('/lineups/' + lineup.lineupId, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });
}

export const createLineupItem = async(lineupItem: LineupItem) => {
    return await fetch('/lineupitems', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...lineupItem }),
    });
}

export const updateLineupItem = async (lineupItem: LineupItem) => {
    return await fetch('/lineupitems/' + lineupItem.lineupItemId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...lineupItem }),
    });
}

export const createQueryLineupItem = async (queryLineupItem: QueryLineupItem) => {
    const response = await fetch('/querylineupitems', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...queryLineupItem }),
    });

    return await response.json();
}

export const deleteQueryLineupItem = async (id: number) => {
    return await fetch('/querylineupitems/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });
}

export const fetchLineupItem = async (id: number): Promise<LineupItem> => {
    const response = await fetch('/lineupitems/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch lineup item: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const fetchQueryLineupItemsByLineupItem = async (id: number) => {
    const response = await fetch('/querylineupitems/lineupItem/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch query lineup items: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.items;
}