import { Container } from "@mui/material"
import type { TemplateSlot, Transition } from '../types';
import { IconButton, Stack } from "@mui/material";
import Typography from '@mui/material/Typography';
import AddIcon from '@mui/icons-material/Add';
import { useState } from 'react'
import CustomForm from '@/components/CustomForm';
import { fields } from '../fields';
import { useQuery } from '@tanstack/react-query';

import { useTransitionUpdateHook } from '../hooks/transitionHooks';
import { slotTypeQueryOptions } from '../api/queryOptions';
import EditingGrid, { type EditingGridColumns } from "../../../components/EditingGrid";

interface TransitionEditLayoutProps {
    transitionData: Transition
}

const TransitionEditLayout = ({ transitionData }: TransitionEditLayoutProps) => {

    const { mutate: updateTransition } = useTransitionUpdateHook();

    const [slotData, setSlotData] = useState<TemplateSlot[]>(transitionData.slots.sort((a, b) => a.index - b.index));

    const { data: slotsTypes, isLoading: slotsLoad } = useQuery(slotTypeQueryOptions());

    const handleSaveTransition = async (item: Transition) => {
        const newItem = { ...item };
        newItem.slots = [...slotData].map(x => { return { ...x, id: x.id < 0 ? 0 : x.id}});
        updateTransition(newItem);
    }

    const formatSlotType = (id: number) => {
        return slotsTypes[id];
    }

    const addSlot = () => {
        const newFilterData = [...slotData, { id: (slotData.length + 1) * -1, slotType: 0, transitionTemplateId: transitionData.id, index: slotData.length + 1 } as TemplateSlot];
        setSlotData(newFilterData);
    }

    const removeSlot = (item: TemplateSlot) => {
        const newSlotData = slotData.filter(x => x.id != item.id)
        setSlotData(newSlotData)
    }

    const saveSlot = (item: TemplateSlot) => {
        setSlotData(slotData.map(slot => slot.id == item.id ? item : slot));
    }

    const handleMove = (item: TemplateSlot, direction: number) => {
        const newSlotData = [...slotData];
        const currentItemIndex = newSlotData.findIndex(x => x.index == item.index);
        console.log(item)
        if (currentItemIndex == -1) return;

        if (currentItemIndex == 0 && direction == -1) return;
        if (currentItemIndex == newSlotData.length - 1 && direction == 1) return;

        const nextItem = newSlotData[currentItemIndex + direction];

        newSlotData[currentItemIndex + direction] = item;
        newSlotData[currentItemIndex] = nextItem;

        setSlotData(newSlotData.map((x, index) => { x.index = index; return x; }));
    }

    const slotgridColumns = [
        { name: 'slotType', display: 'Type', initialValue: 0, type: 'Select' ,options: slotsTypes, format: formatSlotType},
    ] as EditingGridColumns<TemplateSlot>[]

    return (
        <Container sx={{ mt: 5 }}>
            {
                transitionData &&
                <CustomForm title={transitionData.name} save={handleSaveTransition} initialValue={transitionData} fields={fields}>
                    <Stack direction="row" justifyContent="space-between" sx={{m: 1, p:1}}>
                        <Typography variant="h6" >Slots:</Typography>
                        < IconButton onClick={addSlot} >
                            <AddIcon />
                        </IconButton>
                    </Stack>
                        {slotData && <EditingGrid gridItems={slotData} gridFieldColumns={slotgridColumns} deleteItem={removeSlot} saveItem={saveSlot} isLoading={slotsLoad} handleMove={handleMove} />}
                </CustomForm>
            }
        </Container>
    )
}

export default TransitionEditLayout;