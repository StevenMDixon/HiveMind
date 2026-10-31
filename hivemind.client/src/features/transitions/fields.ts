import type { Transition } from './types';
import { type CustomFormField } from '../../components/FormFields';

export const transitionDefault = { id: 0, name: "", matchShowBumps: false } as Transition

export const fields = [
    { name: 'name', type: "Text", initialValue: transitionDefault.name, validator: (transitionName: string) => transitionName != '', required: true, display: "Transittion Name"},
    { name: 'matchShowBumps', type: "Checkbox", initialValue: transitionDefault.matchShowBumps, display: "Match Show Bumpers"}
] as CustomFormField[];