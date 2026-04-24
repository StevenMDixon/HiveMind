import type { Channel } from '../Types/Channel'; 
import type { ApiResponse } from './ApiResponse';

export const fetchChannel = async (id: string | undefined): Promise<ApiResponse<Channel>> => {
    const response = await fetch('/channels/' + id);

    if (!response.ok) {
        return {
            ok: false,
            errorMessage: "Failed to fetch channels"
        } as ApiResponse<Channel>
    }

    const data = await response.json()

    return {
        ok: true,
        data: data.Channel
    } as ApiResponse<Channel>
};

export const fetchChannels = async () => {
    const response = await fetch('/channels');

    if (response.ok) {
        const data = await response.json();
        return data.channels;
    }
};

export const saveChannel = async(channel: Channel) => {
    const result = await fetch('/channels/' + channel.channelID, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...channel }),
    });

    return result;
}

export const createChannel = async (channel: Channel) => {
    return await fetch('/channels', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...channel }),
    });
}

export const deleteChannel = async (channelId: number) => {
    return await fetch(`/channels/${channelId}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });
}