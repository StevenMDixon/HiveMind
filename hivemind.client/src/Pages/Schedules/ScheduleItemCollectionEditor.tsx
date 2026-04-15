
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import AddIcon from '@mui/icons-material/Add';

import { type EditingGridColumns } from '../Components/EditingGrid';
import EditingGrid from '../Components/EditingGrid';

import type { Collection } from '../../Types/Collections' 

import type { CollectionScheduleItem, ScheduleItem } from '../../Types/Schedule';

import { useState, use } from 'react'

import { createCollectionScheduleItem, deleteCollectionScheduleItem, updateScheduleItem } from '../../Api/Schedules';
import type { EnumOptionsObj } from '../../Types/General';


interface ScheduleItemCollectionEditorProps {
    scheduleItem: ScheduleItem
    collectionScheduleItemsPromise: Promise<CollectionScheduleItem[]> | null | undefined;
    collectionTypePromise: Promise<EnumOptionsObj>;
    playoutTypePromise: Promise<EnumOptionsObj>;
    availableCollectionPromise: Promise<Collection[]>
}

function ScheduleItemCollectionEditor({ scheduleItem, collectionScheduleItemsPromise, collectionTypePromise, availableCollectionPromise, playoutTypePromise }: ScheduleItemCollectionEditorProps) {
    const collectionTypes = use(collectionTypePromise);
    const collections = use(availableCollectionPromise);
    const playOutTypes = use(playoutTypePromise);
    const collectionScheduleItems = collectionScheduleItemsPromise ? use(collectionScheduleItemsPromise) : [] as CollectionScheduleItem[];

    const [scheduleCollectionItems, setScheduleCollectionItems] = useState(collectionScheduleItems);

    const getcollectionname = (id: number) => {
        const foundcollection = collections.find(x => x.collectionID == id);
        return foundcollection?.collectionName ?? 'none';
    }

    const getCollectionType = (id: number) => {
        return collectionTypes[id];
    }

    const getPlayoutType = (id: number) => {
        return playOutTypes[id];
    }

    const addItem = async () => {
        const currentIndex = scheduleCollectionItems.length + 1;
        const newItem = { collectionScheduleItemId: currentIndex * -1, scheduleItemId: scheduleItem.scheduleItemId, collectionId: 0, collectionType: 0, playoutType: 0, index: currentIndex, playDuration: 0, padTo: 0, playCount: 0 } as CollectionScheduleItem

        const result = await createCollectionScheduleItem(newItem);

        newItem.collectionScheduleItemId = result;
        setScheduleCollectionItems([...scheduleCollectionItems, newItem]);
    }


    const removeItem = (scheduleCollectionItem: CollectionScheduleItem) => {
        deleteCollectionScheduleItem(scheduleCollectionItem.collectionScheduleItemId);
        setScheduleCollectionItems(scheduleCollectionItems.filter(x => x.collectionScheduleItemId != scheduleCollectionItem.collectionScheduleItemId));
    }

    const handleSave = (scheduleCollectionItem: CollectionScheduleItem) => {
        const itemIdx = scheduleCollectionItems.findIndex(item => item.collectionScheduleItemId == scheduleCollectionItem.collectionScheduleItemId)
        console.log(scheduleCollectionItem)
        const updated = [...scheduleCollectionItems]
        updated[itemIdx] = scheduleCollectionItem

        updateScheduleItem({
            ...scheduleItem, collections: updated
        });

        setScheduleCollectionItems(updated);
    }

    //name: string;
    //initialValue: string | number | boolean;
    //type: EditingGridFieldTypes;
    //validator ?: (x: string | number | boolean) => boolean;
    //valid ?: boolean;
    //options ?: string[];
    //required ?: boolean;

    const columns = [
        { name: 'collectionId', display: 'Collection', initialValue: 0, type: 'Select', format: (e: number) => getcollectionname(e), options: collections.reduce((acc: EnumOptionsObj, current: Collection) => { acc[current.collectionID] = current.collectionName; return acc; }, {}) },
        { name: 'collectionType', display: 'Assigned Type', initialValue: 0, type: 'Select', format: (e: number) => getCollectionType(e), options: collectionTypes },
        { name: 'playoutType', display: 'Playout Type', initialValue: 0, type: 'Select', format: (e: number) => getPlayoutType(e), options: playOutTypes},
        { name: 'playDuration', display: 'Duration', initialValue: 0, type: 'Number', validator: (n: number) => n > -1},
        { name: 'playCount', display: 'Count', initialValue: 0, type: 'Number', validator: (n: number) => n > -1 },
        { name: 'padTo', display: 'Pad To', initialValue: 0, type: 'Number', validator: (n: number) => n > -1 }
    ] as EditingGridColumns[]

    return (
        <Box>
            <IconButton>
                <AddIcon onClick={addItem} />
            </IconButton>
            <EditingGrid gridItems={scheduleCollectionItems} gridFieldColumns={columns} deleteItem={removeItem} saveItem={handleSave} />
        </Box>
    );
}

export default ScheduleItemCollectionEditor;