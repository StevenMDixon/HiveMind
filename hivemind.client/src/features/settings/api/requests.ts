import type { Setting } from '../types';

export const getSettings = async (): Promise<Setting[]> => {
    const response = await fetch('/api/settings');
    if (!response.ok) {
        throw new Error(`Failed to fetch settings: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    console.log(data)
    return data.settings;
};

export const updateSetting = async (setting: Setting) => {
    return await fetch('/api/settings/' + setting.settingId, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...setting }),
    });
}
