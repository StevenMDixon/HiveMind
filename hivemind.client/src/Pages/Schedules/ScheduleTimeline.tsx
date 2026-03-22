import Timeline from '@mui/lab/Timeline';
import TimelineItem from '@mui/lab/TimelineItem';
import TimelineSeparator from '@mui/lab/TimelineSeparator';
import TimelineConnector from '@mui/lab/TimelineConnector';
import TimelineContent from '@mui/lab/TimelineContent';
import TimelineDot from '@mui/lab/TimelineDot';
import TimelineOppositeContent from '@mui/lab/TimelineOppositeContent';
import EditIcon from '@mui/icons-material/Edit';
import { use, useState, useEffect } from 'react';
import { Card, CardContent, IconButton, CardActions, Typography, Stack } from "@mui/material";
import type { ScheduleItem } from './types';
import type { Schedule } from './types';
import AddIcon from '@mui/icons-material/Add';
import LibraryBooksIcon from '@mui/icons-material/LibraryBooks';
import MovieIcon from '@mui/icons-material/Movie';
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';

const ScheduleItemContainer = ({ scheduleItem, selectItem }: { scheduleItem: ScheduleItem, selectItem: (a: ScheduleItem) => void }) => {

    return (
        <TimelineItem key={scheduleItem.scheduleItemId}>
            <TimelineOppositeContent color="text.secondary" >
                09: 30 am
            </TimelineOppositeContent>
            < TimelineSeparator >
                <TimelineDot />
                < TimelineConnector />
            </TimelineSeparator>
            < TimelineContent >
                <Card>
                    <CardContent>
                        <Stack direction="row" spacing={2}
                            sx={{
                                justifyContent: 'center', // Aligns children to the right
                                width: '100%' // Ensures the container spans the full width
                            }}
                        >
                            {scheduleItem.type == 'Block' ? <LibraryBooksIcon /> : <MovieIcon />}
                            <Typography>{scheduleItem.name}</Typography>
                        </Stack>
                    </CardContent>
                    <CardActions
                        sx={{
                            justifyContent: 'center', // Aligns children to the right
                            width: '100%' // Ensures the container spans the full width
                        }}
                    >
                        <IconButton aria-label="Edit" onClick={() => selectItem(scheduleItem)}>
                            <EditIcon />
                        </IconButton>
                        <IconButton aria-label="Move Up" onClick={() => selectItem(scheduleItem)}>
                            <ArrowUpwardIcon />
                        </IconButton>
                        <IconButton aria-label="Move Down" onClick={() => selectItem(scheduleItem)}>
                            <ArrowDownwardIcon />
                        </IconButton>
                    </CardActions>
                </Card>
            </TimelineContent>
        </TimelineItem>

    )
}


interface ScheduleTimelineProps {
    selector: (a: ScheduleItem) => void;
    scheduleItemPromise: Promise<Schedule>;
}

const ScheduleTimeline = ({ selector, scheduleItemPromise }: ScheduleTimelineProps) => {
    const schedule = use(scheduleItemPromise);

    const [scheduleItems, setScheduleItems] = useState<ScheduleItem[]>([]);

    useEffect(() => {
        setScheduleItems(schedule.scheduleItems);
    }, [schedule])

    const AddScheduleItem = () => {
        const currentIdx = scheduleItems.length + 1;
        setScheduleItems([
            ...scheduleItems,
            {
                scheduleItemId: -currentIdx,
                name: 'New ' + currentIdx,
                index: currentIdx,
                type: 'General'
            } as ScheduleItem
        ])
    }

    return (
        <Timeline>
            {scheduleItems && scheduleItems.map((scheduleItem: ScheduleItem) => <ScheduleItemContainer key={scheduleItem.scheduleItemId} scheduleItem={scheduleItem} selectItem={selector} />)}
            <TimelineContent>
                <TimelineSeparator >
                    <IconButton onClick={AddScheduleItem}>
                        <AddIcon />
                    </IconButton>
            </TimelineSeparator >
            </TimelineContent>
        </Timeline>
        )
}

export default ScheduleTimeline;