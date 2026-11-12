export interface Transition {
    id: number;
    name: string;
    matchShowBumps: boolean;
    slots: TemplateSlot[];
}

export interface TemplateSlot {
    id: number;
    slotType: number;
    transitionTemplateId: number;
    index: number;
}