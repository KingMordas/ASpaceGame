/*
 * This file is part of ASpaceGame.
 *
 * Copyright (c) 2025 KingMordas
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.md file in the root directory of this source tree.
 */

namespace ASpaceGame.CoreComponents.DTOs;

public class TestFacilityDto
{
    public bool IsFiringTorpedoes { get; set; }
    public int TorpedoesRequired { get; set; }
    public bool IsFiringProbes { get; set; }
    public int ProbesRequired { get; set; }
    public int TransporterPadsRequired { get; set; }
    public int CargoSpaceRequired { get; set; }
    public int RangeRequired { get; set; }
    public int ShuttlesRequired { get; set; }
}
