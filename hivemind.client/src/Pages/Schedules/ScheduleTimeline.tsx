import Timeline from '@mui/lab/Timeline';
import TimelineItem from '@mui/lab/TimelineItem';
import TimelineSeparator from '@mui/lab/TimelineSeparator';
import TimelineConnector from '@mui/lab/TimelineConnector';
import TimelineContent from '@mui/lab/TimelineContent';
import TimelineDot from '@mui/lab/TimelineDot';
import TimelineOppositeContent from '@mui/lab/TimelineOppositeContent';
import { Card, CardContent, IconButton, CardActions, Typography, Stack } from "@mui/material";
import type { ScheduleItem } from '../../Types/Schedule';
import LibraryBooksIcon from '@mui/icons-material/LibraryBooks';
import MovieIcon from '@mui/icons-material/Movie';
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';
import Delete from '@mui/icons-material/DeleteOutline';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';

interface ScheduleItemContainerProps {
    scheduleItem: ScheduleItem,
    index: number,
    selectItem: (a: ScheduleItem) => void,
    deleteItem: (a: ScheduleItem) => void,
    moveItem: (a: ScheduleItem, i: number) => void 
}

const ScheduleItemContainer = ({ scheduleItem, index, selectItem, deleteItem, moveItem}: ScheduleItemContainerProps) => {

    const time = new Date(2024, 11, 25, 0, 0, 0);

    return (
        <TimelineItem key={scheduleItem.scheduleItemId}>
            <TimelineOppositeContent color="text.secondary" >
                {time.toLocaleTimeString()}
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
                        <IconButton disabled={index == 0} aria-label="Move Up" onClick={() => moveItem(scheduleItem, -1)}>
                            <ArrowUpwardIcon />
                        </IconButton>
                        <IconButton aria-label="Move Down" onClick={() => moveItem(scheduleItem, 1)}>
                            <ArrowDownwardIcon />
                        </IconButton>
                        <IconButton aria-label="Delete" onClick={() => deleteItem(scheduleItem)}>
                            <Delete />
                        </IconButton>
                    </CardActions>
                </Card>
            </TimelineContent>
        </TimelineItem>
    )
}

interface ScheduleTimelineProps {
    selector: (a: ScheduleItem) => void;
    remove: (a: ScheduleItem) => void;
    add: () => void;
    scheduleItems: ScheduleItem[];
    move: (a: ScheduleItem, i: number) => void;
}

const ScheduleTimeline = ({ selector, add, scheduleItems, remove, move}: ScheduleTimelineProps) => {
    return (
        <Timeline>
            {scheduleItems && scheduleItems.map((scheduleItem: ScheduleItem, index: number) =>
                <ScheduleItemContainer key={scheduleItem.scheduleItemId} scheduleItem={scheduleItem} index={index} selectItem={selector} deleteItem={remove} moveItem={move} />)}
            <TimelineItem>
           
                    <TimelineSeparator >
                        <TimelineDot color="primary" variant="outlined" onClick={add}  >
                                <AddIcon />
                        </TimelineDot>
                    
            </TimelineSeparator >
    
        </TimelineItem>
        </Timeline>
        )
}

export default ScheduleTimeline;