import type { Channel } from '../Types/Channel'; 


export const fetchChannel = async (id: string | undefined): Promise<Channel> => {
    const response = await fetch('/channels/' + id);

    if (!response.ok) {
        throw new Error("Failed to Load Channel")
    }

    const data = await response.json()

    return data.channel
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