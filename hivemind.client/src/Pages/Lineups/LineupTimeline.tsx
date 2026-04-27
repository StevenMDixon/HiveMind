import Timeline from '@mui/lab/Timeline';
import TimelineItem from '@mui/lab/TimelineItem';
import TimelineSeparator from '@mui/lab/TimelineSeparator';
import TimelineConnector from '@mui/lab/TimelineConnector';
import TimelineContent from '@mui/lab/TimelineContent';
import TimelineDot from '@mui/lab/TimelineDot';
import TimelineOppositeContent from '@mui/lab/TimelineOppositeContent';
import { Card, CardContent, IconButton, CardActions, Typography, Stack } from "@mui/material";
import type { LineupItem } from '../../Types/Lineup';
import LibraryBooksIcon from '@mui/icons-material/LibraryBooks';
import MovieIcon from '@mui/icons-material/Movie';
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';
import Delete from '@mui/icons-material/DeleteOutline';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';

interface LineupItemContainerProps {
    lineupItem: LineupItem,
    index: number,
    selectItem: (a: LineupItem) => void,
    deleteItem: (a: LineupItem) => void,
    moveItem: (a: LineupItem, i: number) => void,
    currenTime: Date
}

const LineupItemContainer = ({ lineupItem, index, selectItem, deleteItem, moveItem, currenTime}: LineupItemContainerProps) => {
    return (
        <TimelineItem key={lineupItem.lineupItemId}>
            <TimelineOppositeContent color="text.secondary" >
                {currenTime.toLocaleTimeString()}
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
                                justifyContent: 'center',
                                width: '100%'
                            }}
                        >
                            {lineupItem.type == 'Block' ? <LibraryBooksIcon /> : <MovieIcon />}
                            <Typography>{lineupItem.name}</Typography>
                        </Stack>
                    </CardContent>
                    <CardActions
                        sx={{
                            justifyContent: 'center',
                            width: '100%'
                        }}
                    >
                        <IconButton aria-label="Edit" onClick={() => selectItem(lineupItem)}>
                            <EditIcon />
                        </IconButton>
                        <IconButton disabled={index == 0} aria-label="Move Up" onClick={() => moveItem(lineupItem, -1)}>
                            <ArrowUpwardIcon />
                        </IconButton>
                        <IconButton aria-label="Move Down" onClick={() => moveItem(lineupItem, 1)}>
                            <ArrowDownwardIcon />
                        </IconButton>
                        <IconButton aria-label="Delete" onClick={() => deleteItem(lineupItem)}>
                            <Delete />
                        </IconButton>
                    </CardActions>
                </Card>
            </TimelineContent>
        </TimelineItem>
    )
}

interface LineupTimelineProps {
    selector: (a: LineupItem) => void;
    remove: (a: LineupItem) => void;
    add: () => void;
    lineupItems: LineupItem[];
    move: (a: LineupItem, i: number) => void;
    startTime: Date
}

const getTimeAtIndex = (startTime: Date, index: number, items: LineupItem[]) => {
    let timeDelta = 0;

    const d = items.slice(0, index);
    d.forEach(x => timeDelta += x.queries.reduce((acc, cur) => { acc += (cur.queryType == 4 ? cur.playDuration : 0); return acc; }, 0))

    const newDate = new Date(startTime.getTime() + timeDelta * 60000);

    return newDate;
}

const LineupTimeline = ({ selector, add, lineupItems, remove, move, startTime }: LineupTimelineProps) => {
    return (
        <Timeline>
            {lineupItems && lineupItems.map((lineupItem: LineupItem, index: number) =>
                <LineupItemContainer key={lineupItem.lineupItemId} lineupItem={lineupItem} index={index} selectItem={selector} deleteItem={remove} moveItem={move} currenTime={getTimeAtIndex(startTime, index, lineupItems)} />)}
            <TimelineItem>
                < TimelineSeparator >
                    <TimelineDot color="primary" variant="outlined" onClick={add} >
                                <AddIcon />
                    </TimelineDot>
                </TimelineSeparator>
                < TimelineContent >
                </TimelineContent >
            </TimelineItem>

        </Timeline>
        )
}

export default LineupTimeline;