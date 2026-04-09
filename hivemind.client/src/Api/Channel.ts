import type { Channel } from '../Types/Channel'; 

export const fetchChannel = async (id: string | undefined): Promise<Channel> => {
    const response = await fetch('/channels/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch channel: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.channel;
};

export const fetchChannels = async () => {
    const response = await fetch('/channels');

    if (response.ok) {
        const data = await response.json();
        return data.channels;
    } else {
        return []; 
    }
};

export const saveChannel = async (item: Channel) => {
    const result = await fetch('/channels/' + item.channelID, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ChannelName: item.channelName, ChannelNumber: item.channelNumber }),
    });

    return result;
}