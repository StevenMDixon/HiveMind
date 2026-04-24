import type { Channel } from '../../Types/Channel';
import { type CustomFormField } from '../Components/FormFields';

export const channelDefault = { channelId: 0, channelName: "", channelNumber: 1 } as Channel

export const fields = [
    { name: 'channelName', type: "Text", initialValue: channelDefault.channelName, validator: (channelName: string) => channelName != '', required: true, display: "Channel Name"},
    { name: 'channelNumber', type: "Number", initialValue: channelDefault.channelNumber, required: false, validator: (channelNumber: string) => Number(channelNumber) > 0, display: "Channel Number"}
] as CustomFormField[];