// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Reliability",
    "CA2007:Consider calling ConfigureAwait on the awaited task",
    Justification = @"This is a test library, not a generally reusable library. The consumers are known.")]

[assembly: SuppressMessage("Performance",
                    "CA1861: Avoid constant arrays as arguments",
                    Justification = @"Nei test non e' rilevante
                    la perdita di prestazioni dovuti alle instanziazioni")]