using System.ComponentModel.Design;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Tests.LineupSelectionMappper;

public class LineUpSelectionMapperTests
{
    private ProgramStrategy CreateProgram(List<ProgramStrategyLineup>? lineups = null)
    {
        var programStrategy = new ProgramStrategy();
       
        programStrategy.Lineups = new List<ProgramStrategyLineup>();
        
        programStrategy.Lineups.Add( new ProgramStrategyLineup(){ Id = 1, SelectionType = LineupSelectionType.Any, SelectionOption = ""} );

        if(lineups != null) lineups.ForEach(x => programStrategy.Lineups.Add(x));

        return programStrategy;
    }

    [Fact]
    public void Strategy_With_No_Lineup_Uses_Any()
    {
        var programStrategy = CreateProgram();

        var today = DateOnly.FromDateTime(DateTime.Now);

        var result = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, today);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result);
    }


    [Fact]
    public void Strategy_Matches_DateTime()
    {
        var dateStrat =  new ProgramStrategyLineup(){ Id = 2, SelectionType = LineupSelectionType.Date, SelectionOption = "2026-11-23"};

        var programStrategy = CreateProgram([dateStrat]);

        var date = new DateOnly(2026, 11, 23);

        var result = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, date);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Strategy_Matches_Month_And_Day()
    {
        var dateStrat =  new ProgramStrategyLineup(){ Id = 2, SelectionType = LineupSelectionType.MonthAndDay, SelectionOption = "11/23"};

        var programStrategy = CreateProgram([dateStrat]);

        var date = new DateOnly(2026, 11, 23);

        var result = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, date);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Strategy_Matches_Month()
    {
        var dateStrat =  new ProgramStrategyLineup(){ Id = 2, SelectionType = LineupSelectionType.Month, SelectionOption = "11"};

        var programStrategy = CreateProgram([dateStrat]);

        var date = new DateOnly(2026, 11, 23);

        var result = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, date);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result);
    }
    
    [Fact]
    public void Strategy_Matches_DayOfWeek_Name()
    {
        var dateStrat =  new ProgramStrategyLineup(){ Id = 2, SelectionType = LineupSelectionType.DayOfWeek, SelectionOption = "Monday"};

        var programStrategy = CreateProgram([dateStrat]);

        var date = new DateOnly(2026, 11, 23);

        var result = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, date);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Strategy_Matches_Weekday()
    {
        var dateStrat =  new ProgramStrategyLineup(){ Id = 2, SelectionType = LineupSelectionType.Weekday, SelectionOption = ""};

        var programStrategy = CreateProgram([dateStrat]);

        var date = new DateOnly(2026, 11, 23);

        var result = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, date);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Strategy_Matches_Weekend()
    {
        var dateStrat =  new ProgramStrategyLineup(){ Id = 2, SelectionType = LineupSelectionType.Weekday, SelectionOption = ""};

        var programStrategy = CreateProgram([dateStrat]);

        var date = new DateOnly(2026, 10, 23);

        var result = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, date);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Strategy_Matches_Higher_Priority_Item()
    {
        var dateStrat =  new List<ProgramStrategyLineup>(){
            new ProgramStrategyLineup(){ Id = 2, SelectionType = LineupSelectionType.Weekday, SelectionOption = ""},
            new ProgramStrategyLineup(){ Id = 4, SelectionType = LineupSelectionType.MonthAndDay, SelectionOption = "11/23"},
            new ProgramStrategyLineup(){ Id = 3, SelectionType = LineupSelectionType.Date, SelectionOption = "2026-11-23"}
        };

        var programStrategy = CreateProgram([..dateStrat]);

        var date = new DateOnly(2026, 11, 23);

        var result = LineUpSelectionMapper.GetMatchingProgramStrategyLineup(programStrategy, date);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result);
    }
}